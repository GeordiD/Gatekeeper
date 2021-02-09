import { Position } from "@/models/TDA/Position";
import { _authStore } from "@/store/AuthStore";
import axios from "axios";
import { TdaService } from "./TdaService";

export class AccountService extends TdaService {
  async getPositions(): Promise<Position[]> {
    var result = await axios.get("https://api.tdameritrade.com/v1/accounts", {
      params: { fields: "positions" },
      headers: this.getDefaultHeaders(),
    });

    return result.data[0].securitiesAccount.positions;
  }
}
