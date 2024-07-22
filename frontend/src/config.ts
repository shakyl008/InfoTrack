// environment variabe would be set during deployment - dev machine does not have it
const config = {
    searchApi: "https://localhost:7035/api" || process.env.SearchApi
    }

export default config;