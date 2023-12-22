import React, { useState, useEffect } from 'react'
import { Button, Modal, Form, Input, ButtonProps } from 'semantic-ui-react';
import { apiService } from '../api/apiService';
import { Pharmacy } from '../api/types';

type Props = {
    pharmacyData: Pharmacy;
}

const PharmacyModal = (props: Props) => {

    const [pharmacyData, setPharmacyData] = useState<Pharmacy>(props.pharmacyData);
    const[isModalOpen, setIsModalOpen] = useState(false);

    return (
        <>
            <Modal
                open={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                >
                <Modal.Header>Pharmacy Data</Modal.Header>
                <Modal.Content>
                    <Form>
                        <Form.Field>
                            <label>Name</label>
                            <Input
                                value={pharmacyData?.name}
                                />
                        </Form.Field>

                    </Form>
                </Modal.Content>
            </Modal>
        </>
    )

}

export default PharmacyModal