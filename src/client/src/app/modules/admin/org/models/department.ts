export interface Department {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    name: string;
    description: string;
    headOfDepartment: boolean;
    production: number;
    policyId: string;
    parentId?: string;
    parentDept?: string;
}
