import { useState } from 'react';
import { Menu } from 'semantic-ui-react';

const Header = () => {
	const menuArr = ['Pharmacies']

	const [activeItem, setActiveItem] = useState(null);

	const handleItemClick = (e, { name }) => {
		setActiveItem(name);
	}

	return (	
	<Menu pointing inverted>
		{menuArr.map((title, id) => {
		return (
			<Menu.Item
				key={id}
				name={title}
				active={activeItem === title}
				onClick={handleItemClick}
				/>
			)
		})}	
	</Menu>	
	)
}

export default Header