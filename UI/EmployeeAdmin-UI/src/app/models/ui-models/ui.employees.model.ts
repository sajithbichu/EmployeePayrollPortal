import { Address } from "./ui.address.model";
import { Gender } from "./ui.gender.model";

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
