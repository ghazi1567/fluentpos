import { JobType } from "src/app/core/enums/JobType";

export interface Job {
    id: string;
    createaAt: string;
    updatedAt: string;
    organizationId: string;
    branchId: string;
    jobName: JobType;
    schedule: string;
    enabled: boolean;
    job: string;
}
