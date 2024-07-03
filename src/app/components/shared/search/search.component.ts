import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  @Output() onSearch=new EventEmitter<string>();
  searchControl=new FormControl();

  search(){
    this.onSearch.emit(this.searchControl.value);
  }

  ngOnInit(){
    this.searchControl.valueChanges.pipe(filter(s=>s.length==0)).subscribe(res=>{
      this.onSearch.emit(this.searchControl.value);
    })
  }

}
