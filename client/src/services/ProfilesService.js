import { AppState } from "../AppState.js";
import { api } from "./AxiosService.js";
import { logger } from "../utils/Logger.js";
import { Account } from "../models/Account.js";

class ProfilesService {

    async getProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log('Profile page response data', response.data)
        AppState.profile = new Account(response.data)
    }


}
export const profilesService = new ProfilesService()