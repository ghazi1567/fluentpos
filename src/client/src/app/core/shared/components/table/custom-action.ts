import { EventEmitter } from "@angular/core";

export class CustomAction {
    title: string;
    icon: string;
    color: string;
    key: string;
    rights: string;
    constructor(title: string, key: string, rights:string, icon: string = null, color: string = null) {
        this.title = title;
        this.color = color ?? "primary";
        this.icon = icon ?? "build";
        this.key = key;
        this.rights = rights;
    }
}
