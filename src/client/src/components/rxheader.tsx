import { Header, Image } from 'semantic-ui-react';
import rxLogo from '../assets/rx.png';
import '../styles/rxheader.css'

const RxHeader = () => {
	//let env: string = import.meta.env.DEV === true ? 'Dev' : 'Prod';
	let env: string = import.meta.env.MODE;
	return (	
	<div className='background'>
		<div className='header-container'>
			<div className='header'>
				<Image src={rxLogo} alt="header" style={{ width: "8%"}} /> 
				<div className='header-content'>
					<Header size='huge' textAlign='center'>Pharmacy Management Portal</Header>
					<Header sub size='small' >{env} Environment</Header>
				</div>
			</div>
		</div>
	</div>
	)
}

export default RxHeader