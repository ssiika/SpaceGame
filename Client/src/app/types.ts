export interface SaveFile {
  id: number;
  user?: GetUserDto;
  seed: number;
  stage: number;
  distance: number;
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
