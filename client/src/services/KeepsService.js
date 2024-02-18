import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";

class KeepsService {
    async getKeeps() {
        const response = await api.get('api/keeps')
        logger.log('keeps response data:', response.data)
        AppState.keeps = response.data.map(keep => new Keep(keep))
    }

}
export const keepsService = new KeepsService()