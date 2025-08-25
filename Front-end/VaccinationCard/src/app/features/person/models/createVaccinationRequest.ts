export interface CreateVaccinationRequest{
    personId: string,
    vaccineId: string,
    doseNumber: number,
    applicationDate?: string
}