export interface GroupedVaccines{
    vaccineName: string,
    vaccineId: string,
    doses: {
        doseNumber: number,
        applicationDate: string
    }
}