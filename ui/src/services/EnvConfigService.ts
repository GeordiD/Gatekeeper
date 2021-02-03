import { EnvConfig } from "@/models/EnvConfig";
import axios from "axios";

export class EnvConfigService {
    async getConfig(): Promise<EnvConfig> {
        var result = await axios.get<EnvConfig>('/config.json');
        if(result && result.data) {
            return result.data;
        } else {
            console.error("No config.json found");
            return {};
        }
    }
}