import { Store } from "@/models/Store";
import { AuthStoreState } from "@/models/StoreStates/AuthStoreState";

class AuthStore extends Store<AuthStoreState> {
    protected data(): AuthStoreState {
        return {
            code: ""
        };
    }

    setCode(value: string) {
        this.state.code = value;
    }
}

export const _authStore: AuthStore = new AuthStore()