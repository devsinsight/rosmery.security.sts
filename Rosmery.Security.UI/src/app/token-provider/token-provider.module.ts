import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateClientComponent } from './client/create-client/create-client.component';
import { TokenProviderRouting } from './token-provider.routing';
import { UpdateClientComponent } from './client/update-client/update-client.component';
import { CreateApiResourceComponent } from './api-resource/create-api-resource/create-api-resource.component';
import { UpdateApiResourceComponent } from './api-resource/update-api-resource/update-api-resource.component';
import { CreateIdentityResourceComponent } from './identity-resource/create-identity-resource/create-identity-resource.component';
import { UpdateIdentityResourceComponent } from './identity-resource/update-identity-resource/update-identity-resource.component';

@NgModule({
  imports: [
    CommonModule,
    TokenProviderRouting
  ],
  declarations: [
    CreateClientComponent,
    UpdateClientComponent,
    CreateApiResourceComponent,
    UpdateApiResourceComponent,
    CreateIdentityResourceComponent,
    UpdateIdentityResourceComponent
  ]
})
export class TokenProviderModule { }
