import { Guid } from "guid-typescript";
import { UserRole } from "../enums/userRole.enum";

export class User{
    id:Guid;
    firstName:string;
    lastName:string;
    role:UserRole;
}