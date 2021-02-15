import { _authStore } from "@/store/AuthStore";

export class TdaService {
    protected async getDefaultHeaders() {
        const accessToken = await _authStore.getAccessToken();
        return {
            Authorization: `Bearer ${accessToken}`,
        };
    }
}
