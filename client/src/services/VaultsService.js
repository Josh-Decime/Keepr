import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";

class VaultsService {

    async getUsersVaults(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('Users vaults response data:', response.data)
        AppState.vaults = response.data.map(vault => new Vault(vault))
    }



}
export const vaultsService = new VaultsService()