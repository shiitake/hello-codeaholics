export interface Pharmacy {
    id: number,
    name: string,
    address: string,
    city: string,
    state: string,
    zip: number,
    filledPrescriptionsCount: number,
}

export interface Response {
    message: string
}