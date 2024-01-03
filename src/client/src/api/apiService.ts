import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import { Pharmacy, Response } from "./types"

let requestConfig: AxiosRequestConfig = {
    baseURL: import.meta.env.VITE_BASE_URL
}

export const apiService = {
    
    async getPharmacyList(pageNumber: number, postsPerPage: number): Promise<Pharmacy[]> {
        requestConfig.params = {
            pageNumber: pageNumber,
            pageSize: postsPerPage
        }
        
        const response: AxiosResponse<Pharmacy[]> = await axios.get('/api/pharmacy', requestConfig);
        return response.data;
    },

    async updatePharmacy(pharmacy: Pharmacy): Promise<Pharmacy|Response> {
        const response: AxiosResponse<Pharmacy|Response> = await axios.post('/api/pharmacy', pharmacy, requestConfig);
        return response.data;
    }

    
}