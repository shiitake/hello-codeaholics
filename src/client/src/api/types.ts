export interface Pharmacy {
    id: number,
    name: string,
    address: string,
    city: string,
    state: string,
    zip: number,
    filledPrescriptionsCount: number,
    createdDate: Date,
    createdBy: string,
    updatedDate: Date,
    updatedBy: string
}

export interface Response {
    message: string
}