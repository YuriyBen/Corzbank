import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { WalletComponent } from "./header/wallet/wallet.component";
import { HomepageComponent } from "./homepage/homepage.component";

const routes: Routes = [
	{ path: "", component: HomepageComponent },
	{ path: "wallet", component: WalletComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
