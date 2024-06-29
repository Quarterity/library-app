import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
    providedIn:"root"
})
export class BookService{
    constructor(private httpClient: HttpClient) {
    }
    getBooks(){
        return this.httpClient.get(environment.apiUrl+"/books");
    }
}
