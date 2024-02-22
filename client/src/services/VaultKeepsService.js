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


}
export const vaultKeepsService = new VaultKeepsService()