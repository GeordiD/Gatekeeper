import { EnvConfig } from "@/models/EnvConfig";
import { _authStore } from "@/store/AuthStore";
import axios from "axios";
import { EnvConfigService } from "../EnvConfigService";
import qs from 'qs';
import { GetAccessTokenResponse } from "@/models/Responses/TDA/GetAccessTokenResponse";

export class AuthService {

    private _envConfigService: EnvConfigService;

    constructor() {
        this._envConfigService = new EnvConfigService();
    }

    async saveAccessToken(): Promise<boolean> {
        var config = await this._envConfigService.getConfig();
        var client_id = config.tdClientKey;
        var redirect_uri = config.redirect_uri;

        var result = await axios.post(
            `https://api.tdameritrade.com/v1/oauth2/token`,
            qs.stringify({
                grant_type: 'authorization_code',
                refresh_token: '',
                access_type: 'offline',
                code: _authStore.getState().code,
                client_id: client_id,
                redirect_uri: redirect_uri
            }),
            {
                headers: { 
                  "content-type": "application/x-www-form-urlencoded"
                }
              }
        )

        if(result && result.data) {
            _authStore.setTokenResponse(result.data);
            return true;
        } 

        return false;
    }

    async refreshToken(refreshToken: string) {
        var config = await this._envConfigService.getConfig();
        var client_id = config.tdClientKey;

        var result = await axios.post(
            `https://api.tdameritrade.com/v1/oauth2/token`,
            qs.stringify({
                grant_type: 'refresh_token',
                refresh_token: refreshToken,
                client_id: client_id
            }),
            {
                headers: { 
                  "content-type": "application/x-www-form-urlencoded"
                }
              }
        )

        if(result && result.data) {
            console.log(result.data);
            return result.data;
        } else {
            console.log("could not refresh token");
        }
    }
}