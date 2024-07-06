import { Routes } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { bookResolver } from './resolvers/book.resolver';

export const routes: Routes = [
    {
        path:'',
        component:MainComponent,
        resolve:{books:bookResolver}
    }
];
