import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, timer } from "rxjs";

@Injectable({
    providedIn:'root'
})
export class LoadingService {
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private errorSubject = new BehaviorSubject<string | null>(null);
    private successSubject = new BehaviorSubject<string | null>(null);

    loading$=this.loadingSubject.asObservable();
    error$=this.errorSubject.asObservable();
    success$=this.successSubject.asObservable();

    showLoader(){
        this.loadingSubject.next(true);
    }

    hideLoader(){
        this.loadingSubject.next(false);
    }

    hideError(){
        this.errorSubject.next(null);
    }

    showError(err:HttpErrorResponse){
        this.errorSubject.next(err.message);
        timer(5000).subscribe(() => {
            this.hideError();
          });
    }

    hideSuccess(){
        this.successSubject.next(null);
    }

    showSuccess(message:string){
        this.successSubject.next(message);
        timer(2000).subscribe(() => {
            this.hideSuccess();
          });
    }

}