import { Box, Typography } from "@mui/material";
import { SearchDTO } from "./SearchTypes";

const ResultParser: React.FC<{ queryResult: SearchDTO | undefined }> = ({ queryResult }) => {
    
    //adjustment for cases with zero hits
    let hits = queryResult?.positions.match(/,/g)?.length ?? 0;
    if (queryResult?.positions?.length > 2) {
        hits = hits + 1;
    }

    return queryResult ? (
        <Box>
            <Typography align="left">
                Searching for <b>{queryResult.searchQuery}</b> at <b>{queryResult.url}</b> yielded <b>{hits}</b> hits for the term InfoTrack
            </Typography>
            <Typography align="left">
                This is the ranking that each hit occupied:
                <br /> {queryResult.positions}
            </Typography>
        </Box>
    ) : null
}

export default ResultParser;