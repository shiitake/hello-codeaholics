import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import { Pharmacy } from "./types"

let baseUrl: string = "https://localhost:7080";

let mockData: Pharmacy[] = [];
    mockData.push({
        id: 1,
        name: 'Pharmacy Example',
        address: '123 Street',
        city: 'CityName',
        state: 'StateName',
        zip: 12345,
        filledPrescriptionCounts: 100,
        createdDate: new Date(),
        createdBy: 'Admin',
        updatedDate: new Date(),
        updatedBy: 'Admin'
    });

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
    }

    
}