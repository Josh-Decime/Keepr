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

    async getActiveKeep(keepId) {
        let response = await api.get(`api/keeps/${keepId}`)
        let activeKeep = new Keep(response.data)
        AppState.activeKeep = activeKeep
        logger.log('active keep:', AppState.activeKeep)
    }

}
export const keepsService = new KeepsService()