import { GetAccessTokenResponse } from "@/models/Responses/TDA/GetAccessTokenResponse";
import { Store } from "@/models/Store";
import { AuthStoreState } from "@/models/StoreStates/AuthStoreState";

class AuthStore extends Store<AuthStoreState> {
    protected data(): AuthStoreState {
        return {
            code: "",
            tokenResponse: localStorage.getItem('token_response') ? JSON.parse(localStorage.getItem('token_response') as string) : null
        };
    }

    setCode(value: string) {
        this.state.code = value;
    }

    setTokenResponse(value: GetAccessTokenResponse) {
        this.state.tokenResponse = value;
        localStorage.setItem('token_response', JSON.stringify(value));
    }
}

export const _authStore: AuthStore = new AuthStore()