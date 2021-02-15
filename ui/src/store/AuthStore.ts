import { GetAccessTokenResponse } from "@/models/Responses/TDA/GetAccessTokenResponse";
import { Store } from "@/models/Store";
import { AuthStoreState } from "@/models/StoreStates/AuthStoreState";
import { AuthService } from "@/services/TDA/AuthService";
import moment from "moment";

class AuthStore extends Store<AuthStoreState> {
    protected data(): AuthStoreState {
        return {
            code: "",
            tokenResponse: localStorage.getItem("token_response")
                ? JSON.parse(localStorage.getItem("token_response") as string)
                : null,
            tokenExp: localStorage.getItem("token_exp")
                ? moment(localStorage.getItem("token_exp") as string)
                : null
        };
    }

    async getAccessToken(): Promise<string> {
        if (!this.state.tokenResponse) {
            // unexpected. Router should take us to the login page
            throw new Error("No token response saved to the store");
        } else if(!this.state.tokenExp) {
            throw new Error("No token expiration saved");
        }

        if(this.state.tokenExp.isBefore(moment())) {
            // refresh token
            console.log('refreshing token');
            const response = await new AuthService().refreshToken(this.state.tokenResponse.refresh_token);
            
            this.state.tokenResponse.access_token = response.access_token;
            this.state.tokenExp = moment().add(response.expires_in, 's');
            this.saveToLocalStorage();
        }

        return this.state.tokenResponse.access_token;
    }

    setCode(value: string) {
        this.state.code = value;
    }

    setTokenResponse(value: GetAccessTokenResponse) {
        this.state.tokenResponse = value;
        this.state.tokenExp = moment().add(value.expires_in, 's');

        this.saveToLocalStorage();
    }

    private saveToLocalStorage() {
        localStorage.setItem("token_response", JSON.stringify(this.state.tokenResponse));
        localStorage.setItem("token_exp", this.state.tokenExp!.toString());
    }
}

export const _authStore: AuthStore = new AuthStore();
