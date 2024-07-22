import { Alert } from "@mui/material";

const ErrorMessage: React.FC<{ error: boolean, errorMessage: string | undefined }> = ({ error, errorMessage }) => (
    error ? (
        <Alert severity="error">Error: {errorMessage}</Alert>
    ) : null
)

export default ErrorMessage;