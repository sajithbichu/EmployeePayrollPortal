import { Address } from "./address.model";
import { Gender } from "./gender.model";

export interface Employees{
  id:string,
  firstName:string,
  lastName:string,
  dateOfBirth:string,
  email: string,
  mobile: number,
  profileImageUrl: string,
  genderId: string,
  gender: Gender,
  address: Address
}
