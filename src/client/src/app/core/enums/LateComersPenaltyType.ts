export enum LateComersPenaltyType {
    Nothing = 0,
    Hour = 1
}
export const LateComersPenaltyTypeMapping: Record<LateComersPenaltyType, string> = {
    [LateComersPenaltyType.Nothing]: "Do Nothing",
    [LateComersPenaltyType.Hour]: "Hour",
};