import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";
import { Keep, VaultKept } from "../models/Keep.js";

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
        AppState.keeps = response.data.map(keep => new VaultKept(keep))
    }

    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('creating vault response data:', response.data)
        const newVault = new Vault(response.data)
        // FIXME doesn't work for some reason but even if it did it will have the same issues as in keeps
        // AppState.vaults.push(newVault)
        return newVault
    }

    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log('Deleted vault', response)
    }

    async getMyVaults() {
        const response = await api.get(`account/vaults`)
        AppState.myVaults = response.data
        logger.log('My vaults in the AppState:', response.data)
    }



}
export const vaultsService = new VaultsService()