import axios from "axios";
import { SearchDTO } from "../SearchBar/SearchTypes";
import config from "../../config";
import { useState } from "react";
import ErrorMessage from "../SearchBar/ErrorMessage";
import { Container, Button, Typography } from "@mui/material";


type TrendData = {
    searchDate: string;
    url: string;
    score: number;
    searchQuery: string;
};

const calculateScore = (positions: string): number => {

    // clean up the string
    const splitArr = positions.replace(/[{}]/g, "").split(',');
    const numArr: number[] = [];

    splitArr.forEach((item) => {
        const num = Number(item) + 1;
        numArr.push(num);
    });

    // add up scores using a 1/x formula
    let score = 0;
    numArr.forEach((num) => {
        score += 1 / num;
    });

    return score;
}

const parseTrendData = (data: SearchDTO[]): TrendData[] => {

    const trendArr: TrendData[] = [];

    data.forEach((item) => {
        const tempTrendData: TrendData = {
            searchDate: new Date(item.searchDate).toLocaleDateString(),
            url: item.url,
            score: calculateScore(item.positions),
            searchQuery: item.searchQuery
        };

        trendArr.push(tempTrendData);
    })

    return trendArr;
}


const Trend: React.FC = () => {
    const [error, setError] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>();
    const [trendData, setTrendData] = useState<TrendData[]>();

    const handleGetSearchResult = async () => {
        try {
            setError(false);
            const response = await axios.get<SearchDTO[]>(`${config.searchApi}/Search/getsearchresults`);
            const trendData = parseTrendData(response.data);
            setTrendData(trendData)
        }
        catch (err) {
            setError(true);
            setErrorMessage(JSON.stringify(err));
        }
    }
    
    const trendTypography: JSX.Element[] = [];
    trendData?.forEach((item, index) => {
        trendTypography.push(
            <Typography key={index} align="left">
                <b>Day:</b> {item.searchDate}, <b>Score:</b> {item.score.toFixed(3)} - <b>Engine:</b> {item.url}, <b>Search</b> {item.searchQuery}
            </Typography>
        );
    });

    return (
        <>
            <Container>
                <Button variant="contained" onClick={handleGetSearchResult}> Get Trends </Button>
                {trendTypography}
                <ErrorMessage error={error} errorMessage={errorMessage}></ErrorMessage>
            </Container>

        </>
    );
}

export default Trend;