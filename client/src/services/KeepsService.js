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

    async createKeep(keepData) {
        const response = await api.post('api/keeps', keepData)
        logger.log('creating keep response data:', response.data)
        const newKeep = new Keep(response.data)
        // FIXME this will shift it into whatever keeps are active even if it doesn't belong there
        // AppState.keeps.unshift(newKeep)
        return newKeep
    }

    async deleteKeep(keepId) {
        const response = await api.delete(`api/keeps/${keepId}`)
        logger.log('Deleted keep', response)

        AppState.activeKeep = {}
        const indexToRemove = AppState.keeps.findIndex(keep => keep.id == keepId)
        AppState.keeps.splice(indexToRemove, 1)
    }

    async getUsersKeeps(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('users keeps:', response.data)
        AppState.keeps = response.data.map(keep => new Keep(keep))
    }

}
export const keepsService = new KeepsService()