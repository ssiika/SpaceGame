export interface SaveFile {
  Id: number;
  User?: GetUserDto;
  Seed: number;
  Stage: number;
  Distance: number;
}

export interface GetUserDto {
  Id: number;
  Username: string;
}
