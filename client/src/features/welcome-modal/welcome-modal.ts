import { Component, output } from '@angular/core';


@Component({
  selector: 'app-welcome-modal',
  imports: [],
  templateUrl: './welcome-modal.html',
  styleUrl: './welcome-modal.css'
})
export class WelcomeModal {
  onClose = output<void>()


  closeModal() {
    this.onClose.emit()
  }
}
