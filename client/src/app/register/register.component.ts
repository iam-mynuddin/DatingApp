import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  @Input() userForHomeComponent: any;

  model: any = {};
  constructor() { }
  ngOnInit(): void { }

  register() {
    console.log(this.model)
  }
  cancel() {
    alert("Cancelled")
  }

}
