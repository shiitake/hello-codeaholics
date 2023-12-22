import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import { Pharmacy, Response } from "./types"

let baseUrl: string = "https://localhost:7080";

let requestConfig: AxiosRequestConfig = {
    baseURL: baseUrl
}

export const apiService = {
    
    async getPharmacyList(pageNumber: number, postsPerPage: number): Promise<Pharmacy[]> {
        requestConfig.params = {
            pageNumber: pageNumber,
            pageSize: postsPerPage
        }
        
        const response: AxiosResponse<Pharmacy[]> = await axios.get('/api/pharmacy', requestConfig);
        return response.data;
        //return mockData;
    },

    async updatePharmacy(pharmacy: Pharmacy): Promise<Pharmacy|Response> {
        const response: AxiosResponse<Pharmacy|Response> = await axios.post('/api/pharmacy', pharmacy, requestConfig);
        return response.data;
    }

    
}