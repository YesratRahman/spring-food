import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.css']
})
export class ContactPageComponent implements OnInit {

  constructor(private http : HttpClient) { }
  
  ngOnInit() {
  }
  /**
   * Submits the user-entered form for contact.
   *
   * @param contactForm - The NgForm being submitted.
   *
   */
  onSubmit(contactForm: NgForm): void {
    if (contactForm.valid) {
      const email = contactForm.value;
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      this.http.post('https://formspree.io/f/xeqvlnzq',
        { name: email.name, replyto: email.email, message: email.messages },
        { 'headers': headers }).subscribe(
          response => {
            console.log(response);
          }
        );
    }
    var verifySent = <HTMLDivElement>document.getElementById("messageSent");
    verifySent.innerHTML = "Thank You! Your Message Has Been Sent.";
    this.clearForm();
  }
  /**
   * Clears the NgForm.
   *
   * @returns void
   *
   */
  clearForm(): void {
    (<HTMLFormElement>document.getElementById("contactForm")).reset();
  }
}
