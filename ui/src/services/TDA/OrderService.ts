import { Order } from "@/models/TDA/Order";
import axios from "axios";
import moment from "moment";
import { TdaService } from "./TdaService";

export class OrderService extends TdaService {
    async getOrders(fromDate?: moment.Moment, toDate?: moment.Moment): Promise<Order[]> {
        let params: any = {};
        params['status'] = 'FILLED';
        if(fromDate && toDate) {
            params['fromEnteredTime'] = fromDate.format('yyyy-MM-DD');
            params['toEnteredTime'] = toDate.format('yyyy-MM-DD');
        } else {
            params['fromEnteredTime'] = '2020-01-01';
            params['toEnteredTime'] = moment().format('yyyy-MM-DD');
        }

        var result = await axios.get("https://api.tdameritrade.com/v1/orders", {
          params: params,
          headers: await this.getDefaultHeaders(),
        });

        return result.data;
      }
}