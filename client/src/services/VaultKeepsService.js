import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";

class VaultKeepsService {

    async createVaultKeep(VaultKeepData) {
        const response = await api.post('api/vaultKeeps', VaultKeepData)
        logger.log('create vaultKeep response data:', response.data)
    }

    async deleteVaultKeep(vaultKeepId) {
        const response = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
        logger.log('Delete vaultKeep response data:', response.data)
        // FIXME Im not sure how to find index so I can remove the keep from the array
        const indexToRemove = AppState.keeps.findIndex(keep => keep.vaultKeepId == vaultKeepId)
        if (indexToRemove > -1) {
            AppState.keeps.splice(indexToRemove, 1)
        }
    }


}
export const vaultKeepsService = new VaultKeepsService()