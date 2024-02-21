import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";
import { Keep } from "../models/Keep.js";

class VaultsService {

    async getUsersVaults(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('Users vaults response data:', response.data)
        AppState.vaults = response.data.map(vault => new Vault(vault))
    }

    async setActiveVault(vaultId) {
        let response = await api.get(`api/vaults/${vaultId}`)
        let activeVault = new Vault(response.data)
        AppState.activeVault = activeVault
    }

    async getKeepsInVault(vaultId) {
        let response = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('keep in vault response data:', response.data)
        AppState.keeps = response.data.map(keep => new Keep(keep))
    }



}
export const vaultsService = new VaultsService()