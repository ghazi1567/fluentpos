export interface UserBranch {
    userBranchs: UserBranchModel[];
}
export interface UserBranchModel {
    identityUserId: string;
    active: boolean;
    selected: boolean;
    organizationId: string;
    branchId: string;
    branchName: string;
}
