import { GetAccessTokenResponse } from "../Responses/TDA/GetAccessTokenResponse";

export interface AuthStoreState {
    code: string;
    tokenResponse: GetAccessTokenResponse | null;
    tokenExp: moment.Moment | null;
}