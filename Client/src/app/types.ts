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

export interface UserData {
  username: string;
  password: string;
}

export interface UserCreds {
  username: string;
  token: string;
}

export interface ServiceResponse<T> {
  data?: T;
  success: boolean;
  message: string
}
