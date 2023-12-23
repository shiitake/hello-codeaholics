import React, { useState, useEffect } from 'react'
import { Button, Modal, Form, Input, Message, Icon } from 'semantic-ui-react';
import { apiService } from '../api/apiService';
import { Pharmacy, Response } from '../api/types';

interface PharmacyModalProps {
    pharmacy: Pharmacy;
    closeModal: () => void;
    savePharmacy: (pharmacy: Pharmacy) => void;
}

const PharmacyModal: React.FC<PharmacyModalProps> = ({ pharmacy, closeModal, savePharmacy }) => {

    const [updatedPharmacy, setUpdatedPharmacy] = useState<Pharmacy>(pharmacy);
    const [updateError, setUpdateError] = useState(false);

    useEffect(() => {
        pharmacy.id = 100;
        pharmacy.updatedBy = 'portal';
        setUpdatedPharmacy(pharmacy)
    }, [pharmacy]);

    const saveChanges = async () => {
        setUpdateError(false);
        try {
            const response = await apiService.updatePharmacy(updatedPharmacy);
            if ((response as Pharmacy).id !== undefined){
                savePharmacy((response as Pharmacy));
                closeModal();
            }
            else 
            {
                console.log('Error response: ', (response as Response).message);
                setUpdateError(true);
            }
        }
        catch(error){
            setUpdateError(true);
            console.error('Failed updating: ', error)
        }
        
    }

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUpdatedPharmacy({...updatedPharmacy, [event.target.name]: event.target.value});
    }

    const handleDismiss = () => {
        setUpdateError(false);
    }

    return (
        <>
            <Modal
                open={true}
                onClose={closeModal}
                
                >
                <Modal.Header>Pharmacy Data</Modal.Header>
                <Modal.Content>
                    <Message 
                        attached 
                        error 
                        hidden={!updateError}
                        onDismiss={handleDismiss}
                        >
                        <Icon name='exclamation' />
                        There was an error when updating the Pharmacy. Please try again.
                        </Message>
                    <Form>
                        <Form.Field>
                            <label>Name</label>
                            <Input
                                name="name"
                                value={updatedPharmacy.name}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>Address</label>
                            <Input
                                name="address"
                                value={updatedPharmacy.address}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>City</label>
                            <Input
                                name="city"
                                value={updatedPharmacy.city}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>State</label>
                            <Input
                                name="state"
                                value={updatedPharmacy.state}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                        <Form.Field>
                            <label>Zip Code</label>
                            <Input
                                name="zip"
                                value={updatedPharmacy.zip}
                                onChange={handleInputChange}
                                />
                        </Form.Field>
                    </Form>
                </Modal.Content>
                <Modal.Actions>
                    <Button.Group>
                    <Button onClick={closeModal}>Cancel</Button>
                    <Button.Or />
                    <Button positive onClick={saveChanges}>Save</Button>
                    </Button.Group>
                </Modal.Actions>
            </Modal>
        </>
    )

}

export default PharmacyModal