import { Typography, Box, TextField, Button, Container } from '@mui/material'
import React, { useState } from 'react'
import config from "../../config"
import axios from 'axios';
import { SearchDTO } from './SearchTypes';
import ResultParser from './ResultParser';
import ErrorMessage from './ErrorMessage';


const SearchBar: React.FC = () => {
    const initSearchPhrase = "land registry search";
    const [url, setUrl] = useState<string>("https://www.google.co.uk");
    const [search, setSearch] = useState<string>(initSearchPhrase);

    const [error, setError] = useState<boolean>(false);
    const [errorMessage, setErrorMessgae] = useState<string>();

    const [queryResult, setQueryResult] = useState<SearchDTO>();

    const handleSubmit = async () => {
        try {
            setError(false);
            setQueryResult(undefined);
            const response = await axios.get<SearchDTO>(`${config.searchApi}/Search`, { params: { url, search } })
            setQueryResult(response.data.value);
        } catch (err) {
            setError(true);
            setErrorMessgae(JSON.stringify(err?.response?.data));
            console.error(err);
        }

    }

    // this is to allow more flexibility in future
    const handUrlChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setUrl(e.target.value);
    }
    const handleSearchChane = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value)
    }


    return (
        <>
            <Container>
                <Typography variant="h3" align="left" marginBottom="1em">
                    Search
                </Typography>
                <Box component="form" marginBottom="1em">
                    <TextField
                        label="Search engine"
                        value={url}
                        onChange={handUrlChange}
                    ></TextField>
                    <TextField
                        label="Search search, eg: land registry"
                        value={search}
                        onChange={handleSearchChane}
                    ></TextField>
                </Box>
                <Button variant="contained" onClick={handleSubmit}> Submit </Button>
                <ResultParser queryResult={queryResult}></ResultParser>
                <ErrorMessage error={error} errorMessage={errorMessage}></ErrorMessage>
            </Container>

        </>
    )
}

export default SearchBar;