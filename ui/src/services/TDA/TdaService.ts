import { _authStore } from "@/store/AuthStore";

export class TdaService {
  protected getDefaultHeaders() {
    return {
      Authorization: `Bearer ${
        _authStore.getState().tokenResponse!.access_token
      }`,
    };
  }
}
