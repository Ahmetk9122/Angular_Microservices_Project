import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'MicroservicesApp';
  todos: TodoModel[] = [];
  categories: CategoryModel[] = [];
  work?: string;
  name?: string;

  constructor(private http: HttpClient) {
    this.getAllTodos();
    this.getAllCategories();
  }

  getAllTodos() {
    this.http
      .get<TodoModel[]>('http://localhost:5000/api/todos/getall')
      .subscribe((x) => {
        this.todos = x;
      });
  }

  getAllCategories() {
    this.http
      .get<CategoryModel[]>('http://localhost:5000/api/categories/getall')
      .subscribe((x) => {
        this.categories = x;
      });
  }

  saveTodo() {
    this.http
      .get('http://localhost:5000/api/todos/create?work=' + this.work)
      .subscribe((x) => {
        this.getAllTodos();
      });
  }
  saveCategory() {
    const data = {
      name: this.name,
    };
    this.http
      .post('http://localhost:5000/api/categories/create', data)
      .subscribe((x) => {
        this.getAllCategories();
      });
  }
}
export class TodoModel {
  id?: number;
  work?: string;
}
export class CategoryModel {
  id?: number;
  name?: string;
}
