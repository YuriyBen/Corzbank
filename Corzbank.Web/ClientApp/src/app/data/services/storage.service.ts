import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { StorageTypeEnum } from '../enums/storage-type.enum';

@Injectable({
    providedIn: 'root',
})
export class StorageService {

    constructor(private cookieService: CookieService) { }

    setItem(storageType: StorageTypeEnum, key: string, value: string) {
        if (storageType === StorageTypeEnum.LocalStorage) {
            return localStorage.setItem(key, value);
        } else if (storageType === StorageTypeEnum.Cookies) {
            return this.cookieService.set(key, value);
        }
    }

    getItem(storageType: StorageTypeEnum, key: string) {
        if (storageType === StorageTypeEnum.LocalStorage) {
            return localStorage.getItem(key);
        } else if (storageType === StorageTypeEnum.Cookies) {
            return this.cookieService.get(key);
        }
        return null;
    }

    deleteItem(storageType: StorageTypeEnum, key: string) {
        if (storageType === StorageTypeEnum.LocalStorage) {
            return localStorage.removeItem(key);
        } else if (storageType === StorageTypeEnum.Cookies) {
            return this.cookieService.delete(key);
        }
    }

    clear(storageType: StorageTypeEnum) {
        if (storageType === StorageTypeEnum.LocalStorage) {
            return localStorage.clear();
        } else if (storageType === StorageTypeEnum.Cookies) {
            return this.cookieService.deleteAll();
        }
    }
}