import { Header, Image } from 'semantic-ui-react';
import rxLogo from '../assets/rx.png';
import '../styles/rxheader.css'

const RxHeader = () => {
	return (	
	<div className='background'>
		<div className='header-container'>
			<div className='header'>
				<Image src={rxLogo} alt="header" style={{ width: "8%"}} /> 
				<div className='header-content'>
					<Header size='huge' textAlign='center'>Pharmacy Management Portal</Header>
				</div>
			</div>
		</div>
	</div>
	)
}

export default RxHeader