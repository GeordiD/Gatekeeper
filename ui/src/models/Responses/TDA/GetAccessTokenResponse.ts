export interface GetAccessTokenResponse {
    access_token: string,
    refresh_token: string,
    token_type: string,
    expires_in: number,
    scope: string,
    refresh_token_expires_in: number
}