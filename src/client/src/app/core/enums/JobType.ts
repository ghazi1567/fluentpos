export enum JobType {
    MarkAbsent = 1,
    MarkOffDay = 2,
    FetchCheckIn = 3,
    FetchCheckOut = 4,
    AutoSignOff = 5
}
export const JobTypeMapping: Record<JobType, string> = {
    [JobType.MarkAbsent]: "Mark Absent",
    [JobType.MarkOffDay]: "Mark Off Day",
    [JobType.FetchCheckIn]: "Fetch Check In",
    [JobType.FetchCheckOut]: "Fetch Check Out",
    [JobType.AutoSignOff]: "Auto Sign Off"
};